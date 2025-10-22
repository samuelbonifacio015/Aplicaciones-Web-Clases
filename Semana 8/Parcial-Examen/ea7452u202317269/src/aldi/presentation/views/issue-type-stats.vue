<script setup>
import { computed } from 'vue';

/**
 * Define component props with types
 * @type {Prettify<Readonly<ExtractPropTypes<{issueType: {type: String | StringConstructor, required: boolean}, issues: {type: Array | ArrayConstructor, default: function(): []}, equipments: {type: Array | ArrayConstructor, default: function(): []}}>>>}
 * @author Samuel Bonifacio - u202317269
 */
const props = defineProps({
  issueType: {
    type: String,
    required: true
  },
  issues: {
    type: Array,
    default: () => []
  },
  equipments: {
    type: Array,
    default: () => []
  }
});

const filteredIssues = computed(() => {
  return props.issues.filter(issue => issue.issueType === props.issueType);
});

const getEquipmentById = (id) => {
  return props.equipments.find(equipment => equipment.id === id);
};

const costPerHour = computed(() => {
  const openIssues = filteredIssues.value.filter(issue => issue.status === 'OPEN');
  return openIssues.reduce((total, issue) => {
    const equipment = getEquipmentById(issue.equipmentId);
    return total + (equipment ? equipment.costPerHour : 0);
  }, 0);
});

const accumulatedCost = computed(() => {
  const openIssues = filteredIssues.value.filter(issue => issue.status === 'OPEN');
  const now = new Date();
  
  return openIssues.reduce((total, issue) => {
    const equipment = getEquipmentById(issue.equipmentId);
    if (!equipment) return total;
    
    const registeredAt = new Date(issue.registeredAt);
    const hoursElapsed = Math.round((now - registeredAt) / (1000 * 60 * 60)); 
    const cost = equipment.costPerHour * hoursElapsed;
    
    return total + cost;
  }, 0);
});

const reportedIssues = computed(() => {
  return filteredIssues.value.length;
});

const displayIssueType = computed(() => {
  return props.issueType.replace(/_/g, ' ').toLowerCase()
    .replace(/\b\w/g, l => l.toUpperCase());
});
</script>

<template>
  <div class="issue-type-card">
    <div class="card">
      <div class="card-header">
        <h3 class="issue-type-title">{{ displayIssueType }}</h3>
      </div>
      
      <div class="card-content">
        <div class="stats-container">
          <div class="stat-item">
            <span class="stat-label">Cost Per Hour</span>
            <span class="stat-value">${{ costPerHour.toFixed(2) }}</span>
          </div>
          
          <div class="stat-item">
            <span class="stat-label">Accumulated Cost</span>
            <span class="stat-value">${{ accumulatedCost.toFixed(2) }}</span>
          </div>
        </div>
      </div>
      
      <div class="card-footer">
        <span class="reported-issues">
          <i class="pi pi-exclamation-triangle mr-2"></i>
          Reported Issues: {{ reportedIssues }}
        </span>
      </div>
    </div>
  </div>
</template>

<style scoped>
.issue-type-card {
  width: 100%;
  margin-bottom: 1rem;
}

.card {
  background: #ffffff;
  border: 1px solid #e9ecef;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  transition: transform 0.2s, box-shadow 0.2s;
}

.card-header {
  background: #f8f9fa;
  padding: 1rem 1.5rem;
  border-bottom: 1px solid #2563eb;
}

.issue-type-title {
  margin: 0;
  border-bottom: 1px solid #e9ecef;
  font-weight: 600;
  color: #ffffff;
  text-align: center;
  font-size: 1.125rem;
}
  
.card-content {
  color: #495057;
}

.stats-container {
  display: flex;
  gap: 1rem;
}

.stat-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem;
  background: #ffffff;
  border-radius: 6px;
  border-left: 4px solid #3b82f6;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
  border-bottom: 1px solid #f1f3f4;
}

.stat-item:last-child {
  border-bottom: none;
}

.stat-value {
  font-weight: 700;
  color: #6c757d;
  font-size: 0.9rem;
}

.cost-per-hour {
  color: #2c5aa0;
  color: #059669;
  font-size: 1.25rem;
  background: #f8f9fa;
  padding: 1rem 1.5rem;
  border-top: 1px solid #e9ecef;
  text-align: center;
}

.reported-issues {
  color: #475569;
  font-weight: 600;
  font-size: 0.95rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.reported-issues i {
  color: #f59e0b;
}

</style>
