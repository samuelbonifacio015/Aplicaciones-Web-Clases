<script setup>
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { AldiApi } from '@/aldi/infrastructure/aldi-api.js';
import { EquipmentsAssembler } from '@/aldi/infrastructure/equipments.assembler.js';
import { IssuesAssembler } from '@/aldi/infrastructure/issues.assembler.js';
import { ServiceOrdersAssembler } from '@/aldi/infrastructure/service-orders.assembler.js';
import { Issues } from '@/aldi/domain/model/issues.entity.js';
import { ServiceOrders } from '@/aldi/domain/model/service-orders.entity.js';

/**
 * New Issue Component Script
 * @author Samuel Bonifacio - u202317269
 * @type {Router}
 */
const router = useRouter();
const aldiApi = new AldiApi();

const equipments = ref([]);
const issues = ref([]);
const selectedEquipment = ref(null);
const selectedIssueType = ref(null);
const isSubmitting = ref(false);
const errorMessage = ref('');
const isLoading = ref(true);

/**
 * Issue Types Options
 * @type {[{label: string, value: string},{label: string, value: string},{label: string, value: string}]}
 */
const issueTypes = [
  { label: 'No Operation', value: 'NO_OPERATION' },
  { label: 'Wrong Operation', value: 'WRONG_OPERATION' },
  { label: 'Slow Operation', value: 'SLOW_OPERATION' }
];

/**
 * Equipment Options for Select Input
 * @type {ComputedRef<{label: *, value: *, equipment: *}[]>}
 */
const equipmentOptions = computed(() => {
  return equipments.value.map(equipment => ({
    label: equipment.name,
    value: equipment.id,
    equipment: equipment
  }));
});

const isFormValid = computed(() => {
  return selectedEquipment.value !== null && selectedIssueType.value !== null;
});

const getEquipmentById = (id) => {
  return equipments.value.find(equipment => equipment.id === id);
};

const canCreateIssueForEquipment = (equipmentId) => {
  const today = new Date().toISOString().split('T')[0];
  
  return !issues.value.some(issue => {
    const issueDate = new Date(issue.registeredAt).toISOString().split('T')[0];
    return issue.equipmentId === equipmentId && issueDate === today;
  });
};

const loadData = async () => {
  try {
    isLoading.value = true;
    
    const [equipmentsResponse, issuesResponse] = await Promise.all([
      aldiApi.getEquipments(),
      aldiApi.getIssues()
    ]);
    
    equipments.value = EquipmentsAssembler.toEntitiesFromResponse(equipmentsResponse);
    issues.value = IssuesAssembler.toEntitiesFromResponse(issuesResponse);
    
    console.log('Loaded equipments:', equipments.value);
    console.log('Loaded issues:', issues.value);
    
  } catch (error) {
    console.error('Error loading data:', error);
    errorMessage.value = 'Error loading data.';
  } finally {
    isLoading.value = false;
  }
};

const handleCreate = async () => {
  if (!isFormValid.value) {
    errorMessage.value = 'Please select both equipment and issue type';
    return;
  }

  if (!canCreateIssueForEquipment(selectedEquipment.value)) {
    errorMessage.value = 'This equipment already has an issue registered today';
    return;
  }

  isSubmitting.value = true;
  errorMessage.value = '';

  try {
    const equipment = getEquipmentById(selectedEquipment.value);
    
    if (!equipment) {
      throw new Error('Equipment not found');
    }
    
    const issueData = {
      equipmentId: selectedEquipment.value,
      issueType: selectedIssueType.value,
      registeredAt: new Date().toISOString(),
      status: 'OPEN'
    };

    console.log('Creating issue:', issueData);
    
    const issueResponse = await aldiApi.createIssue(issueData);
    const createdIssue = IssuesAssembler.toEntityFromResource(issueResponse.data);
    
    console.log('Created issue:', createdIssue);
    
    const serviceOrderData = {
      equipmentId: selectedEquipment.value,
      issueId: createdIssue.id,
      neededAction: equipment.defaultAction,
      priority: getPriority(equipment.impactInStoreOperations),
      registeredAt: new Date().toISOString()
    };

    console.log('Creating service order:', serviceOrderData);
    
    const serviceOrderResponse = await aldiApi.createServiceOrder(serviceOrderData);
    const createdServiceOrder = ServiceOrdersAssembler.toEntityFromResource(serviceOrderResponse.data);
    
    console.log('Created service order:', createdServiceOrder);
    
    router.push('/home');
    
  } catch (error) {
    console.error('Error creating issue:', error);
  } finally {
    isSubmitting.value = false;
  }
};

const handleCancel = () => {
  router.push('/home');
};

const getPriority = (impact) => {
  switch (impact) {
    case 'TOTAL':
      return 'HIGH';
    case 'PARTIAL':
      return 'NORMAL';
    case 'NONE':
      return 'LOW';
    default:
      return 'NORMAL';
  }
};

onMounted(() => {
  loadData();
});
</script>

<template>
  <div class="new-issue-container">
    <h2 class="text-3xl font-bold mb-2">New Issue</h2>
    <p class="text-lg text-600 mb-4">Add an Issue Record</p>
    
    <div v-if="isLoading" class="text-center">
      <i class="pi pi-spinner pi-spin text-4xl"></i>
      <p class="mt-3">Loading data...</p>
    </div>
    
    <div v-else class="card">
      <form @submit.prevent="handleCreate" class="flex flex-column gap-4">
        <div class="field">
          <label for="equipment" class="block text-900 font-medium mb-2">
            Equipment *
          </label>
          <select
            id="equipment"
            v-model="selectedEquipment"
            class="w-full p-inputtext p-component"
            :class="{ 'p-invalid': selectedEquipment === null && errorMessage }"
          >
            <option value="">Select an equipment</option>
            <option 
              v-for="equipment in equipments" 
              :key="equipment.id" 
              :value="equipment.id"
            >
              {{ equipment.name }}
            </option>
          </select>
        </div>

        <div class="field">
          <label for="issueType" class="block text-900 font-medium mb-2">
            Issue Type *
          </label>
          <select
            id="issueType"
            v-model="selectedIssueType"
            class="w-full p-inputtext p-component"
            :class="{ 'p-invalid': selectedIssueType === null && errorMessage }"
          >
            <option value="">Select an issue type</option>
            <option 
              v-for="type in issueTypes" 
              :key="type.value" 
              :value="type.value"
            >
              {{ type.label }}
            </option>
          </select>
        </div>

        <div 
          v-if="errorMessage" 
          class="p-message p-message-error mb-3"
        >
          <div class="p-message-wrapper">
            <span class="p-message-icon pi pi-times-circle"></span>
            <div class="p-message-text">{{ errorMessage }}</div>
          </div>
        </div>

        <div class="flex justify-content-between gap-3">
          <button
            type="button"
            @click="handleCancel"
            class="p-button p-button-secondary flex-1"
          >
            Cancel
          </button>
          <button
            type="submit"
            :disabled="!isFormValid || isSubmitting"
            class="p-button p-button-primary flex-1"
            :class="{ 'p-button-loading': isSubmitting }"
          >
            <i v-if="isSubmitting" class="pi pi-spinner pi-spin mr-2"></i>
            Create
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
.new-issue-container {
  padding: 2rem;
  max-width: 600px;
  margin: 0 auto;
}

.card {
  background: #ffffff;
  border: 1px solid #e9ecef;
  border-radius: 6px;
  padding: 2rem;
  box-shadow: 0 2px 1px -1px rgba(0,0,0,.2), 0 1px 1px 0 rgba(0,0,0,.14), 0 1px 3px 0 rgba(0,0,0,.12);
}

.field {
  margin-bottom: 1rem;
}

.p-inputtext {
  padding: 0.75rem;
  border: 1px solid #ced4da;
  border-radius: 6px;
  transition: border-color 0.2s, box-shadow 0.2s;
}

.p-inputtext:focus {
  outline: 0 none;
  outline-offset: 0;
  box-shadow: 0 0 0 0.2rem #bfdbfe;
  border-color: #3b82f6;
}

.p-invalid {
  border-color: #e24c4c !important;
}

.p-button {
  display: inline-flex;
  cursor: pointer;
  user-select: none;
  align-items: center;
  vertical-align: bottom;
  text-align: center;
  overflow: hidden;
  position: relative;
  color: #ffffff;
  background: #3b82f6;
  border: 1px solid #3b82f6;
  padding: 0.75rem 1rem;
  font-size: 1rem;
  transition: background-color 0.2s, color 0.2s, border-color 0.2s, box-shadow 0.2s;
  border-radius: 6px;
  text-decoration: none;
  justify-content: center;
}

.p-button:hover {
  background: #2563eb;
  border-color: #2563eb;
}

.p-button-secondary {
  background: #6c757d;
  border-color: #6c757d;
}

.p-button-secondary:hover {
  background: #5c636a;
  border-color: #5c636a;
}

.p-button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.p-message {
  margin: 0;
  display: flex;
  align-items: center;
  padding: 1rem 1.25rem;
  border-radius: 6px;
}

.p-message-error {
  background: #fef2f2;
  border-width: 0 0 0 4px;
  color: #991b1b;
}

.p-message-wrapper {
  display: flex;
  align-items: center;
}

.p-message-icon {
  font-size: 1.25rem;
  margin-right: 0.5rem;
}

.flex {
  display: flex;
}

.flex-column {
  flex-direction: column;
}

.gap-3 {
  gap: 0.75rem;
}

.gap-4 {
  gap: 1rem;
}

.justify-content-between {
  justify-content: space-between;
}

.flex-1 {
  flex: 1 1 0%;
}

.text-center {
  text-align: center;
}

.mt-3 {
  margin-top: 0.75rem;
}

.mb-2 {
  margin-bottom: 0.5rem;
}

.mb-3 {
  margin-bottom: 0.75rem;
}

.mb-4 {
  margin-bottom: 1rem;
}

.mr-2 {
  margin-right: 0.5rem;
}

.w-full {
  width: 100%;
}

.text-3xl {
  font-size: 1.875rem;
  line-height: 2.25rem;
}

.text-lg {
  font-size: 1.125rem;
  line-height: 1.75rem;
}

.text-4xl {
  font-size: 2.25rem;
  line-height: 2.5rem;
}

.text-600 {
  color: #6b7280;
}

.text-900 {
  color: #111827;
}

.font-bold {
  font-weight: 700;
}

.font-medium {
  font-weight: 500;
}

.block {
  display: block;
}
</style>