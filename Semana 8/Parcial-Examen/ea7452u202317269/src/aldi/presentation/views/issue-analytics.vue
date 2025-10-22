<script setup>
import { ref, computed, onMounted } from 'vue';
import { AldiApi } from '@/aldi/infrastructure/aldi-api.js';
import { EquipmentsAssembler } from '@/aldi/infrastructure/equipments.assembler.js';
import { IssuesAssembler } from '@/aldi/infrastructure/issues.assembler.js';
import IssueTypeStats from './issue-type-stats.vue';

/**
 * Instantiate AldiApi
 * Issue Analytics Component
 * @type {AldiApi}
 * @author Samuel Bonifacio - u202317269
 */
const aldiApi = new AldiApi();

const equipments = ref([]);
const issues = ref([]);
const isLoading = ref(true);
const errorMessage = ref('');

const issueTypes = ['NO_OPERATION', 'WRONG_OPERATION', 'SLOW_OPERATION'];

/**
 * Load equipments and issues data
 * @returns {Promise<void>}
 */
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
    errorMessage.value = 'Error loading analytics data. Please refresh the page.';
  } finally {
    isLoading.value = false;
  }
};

onMounted(() => {
  loadData();
});
</script>

<template>
  <div class="issue-analytics-container">
    <h2 class="analytics-title">Issue Analytics</h2>
    
    <div v-if="isLoading" class="loading-container">
      <i class="pi pi-spinner pi-spin text-4xl"></i>
    </div>
    
    <div v-else-if="errorMessage" class="error-container">
      <div class="error-message">
        <i class="pi pi-exclamation-triangle mr-2"></i>
        {{ errorMessage }}
      </div>
    </div>
    
    <div v-else class="analytics-grid">
      <IssueTypeStats
        v-for="issueType in issueTypes"
        :key="issueType"
        :issue-type="issueType"
        :issues="issues"
        :equipments="equipments"
      />
    </div>
  </div>
</template>

<style scoped>
.issue-analytics-container {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.analytics-title {
  font-size: 2rem;
  font-weight: 700;
  text: white;
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
  font-size: 1.125rem;
  margin-top: 1rem;
}

.error-container {
  text-align: center;
  padding: 2rem;
}

.error-message {
  background: #fef2f2;
  border: 1px solid #fecaca;
  color: #991b1b;
  padding: 1rem 1.5rem;
  border-radius: 8px;
  display: inline-flex;
  align-items: center;
  font-weight: 500;
}

.analytics-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 2rem;
  margin-top: 1rem;
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

.issue-type-title{
  color: black;
}

</style>