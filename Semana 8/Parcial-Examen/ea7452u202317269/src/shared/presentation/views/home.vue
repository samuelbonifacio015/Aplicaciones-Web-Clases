<script setup>
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { AldiApi } from '@/aldi/infrastructure/aldi-api.js';
import { ServiceOrdersAssembler } from '@/aldi/infrastructure/service-orders.assembler.js';
import { EquipmentsAssembler } from '@/aldi/infrastructure/equipments.assembler.js';
import { IssuesAssembler } from '@/aldi/infrastructure/issues.assembler.js';

const router = useRouter();
const aldiApi = new AldiApi();

import {useI18n} from "vue-i18n";
const { t } = useI18n();

const serviceOrders = ref([]);
const equipments = ref([]);
const issues = ref([]);
const isLoading = ref(true);
const errorMessage = ref('');

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
    
  } catch (error) {
    console.error('Error loading data:', error);
    errorMessage.value = 'Error loading data. Please refresh the page.';
  } finally {
    isLoading.value = false;
  }
};

const nextServiceOrder = computed(() => {
  const highPriorityOrders = serviceOrders.value.filter(order => order.priority === 'HIGH');
  
  if (highPriorityOrders.length === 0) return null;
  
  // Sort by registeredAt to get the oldest
  return highPriorityOrders.sort((a, b) => 
    new Date(a.registeredAt) - new Date(b.registeredAt)
  )[0];
});

const getEquipmentById = (id) => {
  return equipments.value.find(equipment => equipment.id === id);
};

const getIssueById = (id) => {
  return issues.value.find(issue => issue.id === id);
};

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

const formatIssueType = (issueType) => {
  if (!issueType) return 'N/A';
  return issueType.replace(/_/g, ' ').toLowerCase()
    .replace(/\b\w/g, l => l.toUpperCase());
};

const navigateToServiceOrders = () => {
  router.push('/service-orders');
};

const navigateToAnalytics = () => {
  router.push('/analytics');
};

onMounted(() => {
  loadData();
});
</script>

<template>
  <div class="home-container">
    <h1>{{  t('home.title') }}</h1>
    <p>{{ t('home.content')}}</p> 
    
    <div v-if="isLoading" class="loading-container">
      <i class="pi pi-spinner pi-spin text-4xl"></i>
      <p class="mt-3">Loading data...</p>
    </div>
    
    <div v-else-if="errorMessage" class="error-container">
      <div class="error-message">
        <i class="pi pi-exclamation-triangle mr-2"></i>
      </div>
    </div>
    
    <div v-else>
      <div class="next-service-order-section">
        <h2 class="section-title">Next Service Order</h2>
        
        <div v-if="nextServiceOrder" class="service-order-card">
          <div class="card-header">
            <div class="order-priority">
              <i class="pi pi-exclamation-circle priority-high"></i>
              <span class="priority-badge priority-high">HIGH</span>
            </div>
            <div class="order-id">
              Order #{{ nextServiceOrder.id }}
            </div>
          </div>
          
          <div class="card-content">
            <div class="order-details">
              <div class="detail-row">
                <span class="detail-label">Equipment:</span>
                <span class="detail-value">
                  {{ getEquipmentById(nextServiceOrder.equipmentId)?.name || 'Unknown Equipment' }}
                </span>
              </div>
              
              <div class="detail-row">
                <span class="detail-label">Issue Type:</span>
                <span class="detail-value">
                  {{ formatIssueType(getIssueById(nextServiceOrder.issueId)?.issueType) }}
                </span>
              </div>
              
              <div class="detail-row">
                <span class="detail-label">Action Needed:</span>
                <span class="detail-value action-badge">
                  {{ nextServiceOrder.neededAction }}
                </span>
              </div>
              
              <div class="detail-row">
                <span class="detail-label">Registered:</span>
                <span class="detail-value">
                  {{ formatDate(nextServiceOrder.registeredAt) }}
                </span>
              </div>
            </div>
          </div>
        </div>
        
        <div v-else class="no-service-order">
          <i class="pi pi-check-circle text-6xl"></i>
          <p>All high priority issues have been addressed.</p>
        </div>
      </div>
      
      <div class="navigation-section">
        
        <div class="action-buttons">
                    
          <button @click="navigateToServiceOrders" class="action-button secondary">
            <i class="pi pi-list mr-2"></i>
            Service Orders
          </button>
          
          <button @click="navigateToAnalytics" class="action-button tertiary">
            <i class="pi pi-chart-bar mr-2"></i>
            Analytics
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.home-container {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
  background: linear-gradient(135deg, #f0f9ff, #e0f2fe);
  min-height: calc(100vh - 200px);
  border-radius: 12px;
}

.home-container h1 {
  font-size: 2.5rem;
  font-weight: 700;
  color: #1e3a8a;
  margin-bottom: 1rem;
  text-align: center;
}

.home-container p {
  font-size: 1.25rem;
  color: #334155;
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

.next-service-order-section {
  margin-bottom: 3rem;
}

.section-title {
  font-size: 1.5rem;
  font-weight: 600;
  color: #1e40af;
  margin-bottom: 1.5rem;
}

.service-order-card {
  background: #ffffff;
  border: 2px solid #3b82f6;
  border-radius: 12px;
  overflow: hidden;
}

.card-header {
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

.order-id {
  font-weight: 600;
  color: #ffffff;
}

.card-content {
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
  background: #ffffff;
  border-radius: 8px;
  border-left: 4px solid #3b82f6;
  box-shadow: 0 2px 4px rgba(59, 130, 246, 0.1);
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

.no-service-order {
  text-align: center;
  padding: 3rem;
  color: #475569;
}

.no-service-order i {
  color: #10b981;
  margin-bottom: 1rem;
}

.no-service-order h3 {
  font-size: 1.5rem;
  margin-bottom: 0.5rem;
  color: #334155;
}

.navigation-section {
  margin-top: 3rem;
}

.action-buttons {
  display: flex;
  gap: 1rem;
  justify-content: center;
  flex-wrap: wrap;
}

.action-button {
  display: inline-flex;
  align-items: center;
  padding: 1rem 2rem;
  border-radius: 8px;
  font-weight: 600;
  text-decoration: none;
  transition: all 0.2s;
  border: none;
  cursor: pointer;
  font-size: 1rem;
}

.action-button.primary {
  background: #3b82f6;
  color: #ffffff;
}

.action-button.secondary {
  background: #6b7280;
  color: #ffffff;
}

.action-button.tertiary {
  background: #059669;
  color: #ffffff;
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

</style>