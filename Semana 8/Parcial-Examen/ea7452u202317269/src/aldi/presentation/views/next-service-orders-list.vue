<script setup>
import { ref, computed, onMounted } from 'vue';
import { AldiApi } from '@/aldi/infrastructure/aldi-api.js';
import { EquipmentsAssembler } from '@/aldi/infrastructure/equipments.assembler.js';
import { ServiceOrdersAssembler } from '@/aldi/infrastructure/service-orders.assembler.js';
import { IssuesAssembler } from '@/aldi/infrastructure/issues.assembler.js';

/**
 * @description
 * Instance of AldiApi to fetch data.
 * @type {AldiApi}
 * @author Samuel Bonifacio - u202317269
 */
const aldiApi = new AldiApi();

const serviceOrders = ref([]);
const equipments = ref([]);
const issues = ref([]);
const isLoading = ref(true);
const errorMessage = ref('');

/**
 * @description
 * Loads service orders, equipments, and issues data from the API.
 * @returns {Promise<void>}
 */
const loadData = async () => {
  try {
    isLoading.value = true;
    
    const [serviceOrdersResponse, equipmentsResponse, issuesResponse] = await Promise.all([
      aldiApi.getServiceOrders(),
      aldiApi.getEquipments(),
      aldiApi.getIssues()
    ]);
    
    serviceOrders.value = ServiceOrdersAssembler.toEntitiesFromResponse(serviceOrdersResponse);
    equipments.value = EquipmentsAssembler.toEntitiesFromResponse(equipmentsResponse);
    issues.value = IssuesAssembler.toEntitiesFromResponse(issuesResponse);
    
    console.log('Loaded service orders:', serviceOrders.value);
    console.log('Loaded equipments:', equipments.value);
    console.log('Loaded issues:', issues.value);
    
  } catch (error) {
    console.error('Error loading data:', error);
    errorMessage.value = 'Error loading service orders data. Please refresh the page.';
  } finally {
    isLoading.value = false;
  }
};

const getEquipmentById = (id) => {
  return equipments.value.find(equipment => equipment.id === id);
};

const getIssueById = (id) => {
  return issues.value.find(issue => issue.id === id);
};

const sortedServiceOrders = computed(() => {
  const priorityOrder = { 'HIGH': 1, 'NORMAL': 2, 'LOW': 3 };
  
  return [...serviceOrders.value].sort((a, b) => {
    const priorityDiff = (priorityOrder[a.priority] || 99) - (priorityOrder[b.priority] || 99);
    if (priorityDiff !== 0) return priorityDiff;
    
    return new Date(b.registeredAt) - new Date(a.registeredAt);
  });
});

const formatDate = (dateString) => {
  const date = new Date(dateString);
  return date.toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  });
};

const getPriorityClass = (priority) => {
  switch (priority) {
    case 'HIGH':
      return 'priority-high';
    case 'NORMAL':
      return 'priority-normal';
    case 'LOW':
      return 'priority-low';
    default:
      return 'priority-normal';
  }
};

const getPriorityIcon = (priority) => {
  switch (priority) {
    case 'HIGH':
      return 'pi-exclamation-circle';
    case 'NORMAL':
      return 'pi-info-circle';
    case 'LOW':
      return 'pi-minus-circle';
    default:
      return 'pi-info-circle';
  }
};

const formatIssueType = (issueType) => {
  if (!issueType) return 'N/A';
  return issueType.replace(/_/g, ' ').toLowerCase()
    .replace(/\b\w/g, l => l.toUpperCase());
};

onMounted(() => {
  loadData();
});
</script>

<template>
  <div class="service-orders-container">
    <h2 class="orders-title">Next Service Orders</h2>
    
    <div v-if="isLoading" class="loading-container">
      <i class="pi pi-spinner pi-spin text-4xl"></i>
      <p class="mt-3">Loading service orders...</p>
    </div>
    
    <div v-else-if="errorMessage" class="error-container">
      <div class="error-message">
        <i class="pi pi-exclamation-triangle mr-2"></i>
        {{ errorMessage }}
      </div>
    </div>
    
    <div v-else-if="sortedServiceOrders.length === 0" class="empty-state">
      <i class="pi pi-clipboard text-6xl"></i>
      <h3>No Service Orders</h3>
      <p>There are currently no service orders to display.</p>
    </div>
    
    <div v-else class="orders-list">
      <div 
        v-for="order in sortedServiceOrders" 
        :key="order.id" 
        class="order-card"
      >
        <div class="order-header">
          <div class="order-priority">
            <i :class="['pi', getPriorityIcon(order.priority), getPriorityClass(order.priority)]"></i>
            <span :class="['priority-badge', getPriorityClass(order.priority)]">
              {{ order.priority }}
            </span>
          </div>
          <div class="order-id">
            Order #{{ order.id }}
          </div>
        </div>
        
        <div class="order-content">
          <div class="order-details">
            <div class="detail-row">
              <span class="detail-label">Equipment:</span>
              <span class="detail-value">
                {{ getEquipmentById(order.equipmentId)?.name || 'Unknown Equipment' }}
              </span>
            </div>
            
            <div class="detail-row">
              <span class="detail-label">Issue Type:</span>
              <span class="detail-value">
                {{ formatIssueType(getIssueById(order.issueId)?.issueType) }}
              </span>
            </div>
            
            <div class="detail-row">
              <span class="detail-label">Action Needed:</span>
              <span class="detail-value action-badge">
                {{ order.neededAction }}
              </span>
            </div>
            
            <div class="detail-row">
              <span class="detail-label">Registered:</span>
              <span class="detail-value">
                {{ formatDate(order.registeredAt) }}
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.service-orders-container {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
  background: linear-gradient(135deg, #f0f9ff, #e0f2fe);
  min-height: calc(100vh - 200px);
  border-radius: 12px;
}

.orders-title {
  font-size: 2rem;
  font-weight: 700;
  color: #1e40af;
  margin-bottom: 2rem;
  text-align: center;
}

.loading-container {
  text-align: center;
  padding: 3rem;
}

.loading-container i {
  color: #3b82f6;
  font-size: 3rem;
}

.loading-container p {
  color: #475569;
  font-size: 1.125rem;
  margin-top: 1rem;
}

.error-container {
  text-align: center;
  padding: 2rem;
}

.error-message {
  background: #fef2f2;
  border: 2px solid #fecaca;
  color: #991b1b;
  padding: 1rem 1.5rem;
  border-radius: 8px;
  display: inline-flex;
  align-items: center;
  font-weight: 500;
}

.empty-state {
  text-align: center;
  padding: 4rem 2rem;
  color: #475569;
}

.empty-state i {
  color: #94a3b8;
  margin-bottom: 1rem;
}

.empty-state h3 {
  font-size: 1.5rem;
  margin-bottom: 0.5rem;
  color: #334155;
}

.orders-list {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.order-card {
  border-radius: 12px;
  overflow: hidden;
}

.order-header {
  background: linear-gradient(135deg, #3b82f6, #2563eb);
  padding: 1rem 1.5rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.order-priority {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.priority-badge {
  padding: 0.25rem 0.75rem;
  border-radius: 20px;
  font-size: 0.75rem;
  font-weight: 600;
  text-transform: uppercase;
}

.priority-high {
  color: #dc3545;
}

.priority-high.priority-badge {
  background: #f8d7da;
  color: #721c24;
}

.priority-normal {
  color: #ffc107;
}

.priority-normal.priority-badge {
  background: #fff3cd;
  color: #856404;
}

.priority-low {
  color: #28a745;
}

.priority-low.priority-badge {
  background: #d4edda;
  color: #155724;
}

.order-id {
  font-weight: 600;
  color: #ffffff;
}

.order-content {
  padding: 1.5rem;
  background: #f8fafc;
}

.order-details {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

.detail-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0.75rem;
  border-radius: 8px;
}

.detail-label {
  font-weight: 600;
  color: #475569;
  min-width: 120px;
}

.detail-value {
  color: #1e293b;
  font-weight: 500;
  text-align: right;
}

.action-badge {
  background: linear-gradient(135deg, #bfdbfe, #93c5fd);
  color: #1e40af;
  padding: 0.5rem 1rem;
  border-radius: 20px;
  font-size: 0.9rem;
  font-weight: 600;
  border: 1px solid #3b82f6;
}

.mr-2 {
  margin-right: 0.5rem;
}

.mt-3 {
  margin-top: 0.75rem;
}

.text-4xl {
  font-size: 2.25rem;
  line-height: 2.5rem;
}

.text-6xl {
  font-size: 3.75rem;
  line-height: 1;
}

</style>