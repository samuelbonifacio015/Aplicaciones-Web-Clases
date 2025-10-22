/**
 * Pinia store for managing equipments, issues & service orders in the ALDI context.
 * Encapsulates all CRUD operations and state management for equipments, issues & service orders.
 * 
 * @module useAldiStore
 * 
 * @author - Samuel Bonifacio - u202317269
 */
import {computed, ref} from "vue";
import {AldiApi} from "@/aldi/infrastructure/aldi-api.js";
import {EquipmentsAssembler} from "@/aldi/infrastructure/equipments.assembler.js";
import {IssuesAssembler} from "@/aldi/infrastructure/issues.assembler.js";
import {ServiceOrdersAssembler} from "@/aldi/infrastructure/service-orders.assembler.js";
import {defineStore} from "pinia";

/**
 * Store for publishing context.
 */

const aldiApi = new AldiApi();

const useAldiStore = defineStore('aldi', () => {
    const equipments = ref([]);
    const issues = ref([]);
    const serviceOrders = ref([]);
    const errors = ref([]);
    
    const equipmentsLoaded = ref(false);
    const issuesLoaded = ref(false);
    const serviceOrdersLoaded = ref(false);
    
    const equipmentsCount = computed(() => {
        return equipmentsLoaded ? equipments.value.length : 0;
    });
    
    const issuesCount = computed(() => {
        return issuesLoaded ? issues.value.length : 0;
    });
    
    const serviceOrdersCount = computed(() => {
        return serviceOrdersLoaded ? serviceOrders.value.length : 0;
    });
    
    function fetchEquipments() {
        aldiApi.getEquipments().then(response => {
            equipments.value = EquipmentsAssembler.toEntitiesFromResponse(response);
            equipmentsLoaded.value = true;
            console.log(equipmentsLoaded.value);
            console.log(equipments.value);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function fetchIssues() {
        aldiApi.getIssues().then(response => {
            issues.value = IssuesAssembler.toEntitiesFromResponse(response);
            issuesLoaded.value = true;
            console.log(issuesLoaded.value);
            console.log(issues.value);
        }).catch(error => {
            errors.value.push(error);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function fetchServiceOrders() {
        aldiApi.getServiceOrders().then(response => {
            serviceOrders.value = ServiceOrdersAssembler.toEntitiesFromResponse(response);
            serviceOrdersLoaded.value = true;
            console.log(serviceOrdersLoaded.value);
            console.log(serviceOrders.value);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function getEquipmentById(id) {
        let idNum = parseInt(id);
        return equipments.value.find(equipments => equipments['id'] === idNum);
    }
    
    function addEquipment(equipment) {
        aldiApi.createEquipment(equipment).then(response => {
            const resource = response.data;
            const newEquipment = EquipmentsAssembler.toEntityFromResource(resource);
            equipments.value.push(newEquipment);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function updateEquipment(equipment) {
        aldiApi.updateEquipment(equipment).then(response => {
            const resource = response.data;
            const updatedEquipment = EquipmentsAssembler.toEntityFromResource(resource);
            const index = equipments.value.findIndex(e => e['id'] === updatedEquipment.id);
            if (index !== -1) equipments.value[index] = updatedEquipment;
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function deleteEquipment(equipment) {
        aldiApi.deleteEquipment(equipment.id).then(() => {
            const index = equipments.value.findIndex(e => e['id'] === equipment.id);
            if (index !== -1) equipments.value.splice(index, 1);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function getIssueById(id) {
        let idNum = parseInt(id);
        return issues.value.find(issues => issues['id'] === idNum);
    }
    
    function addIssue(issue) {
        aldiApi.createIssue(issue).then(response => {
            const resource = response.data;
            const newIssue = IssuesAssembler.toEntityFromResource(resource);
            issues.value.push(newIssue);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function updateIssue(issue) {
        aldiApi.updateIssue(issue).then(response => {
            const resource = response.data;
            const updatedIssue = IssuesAssembler.toEntityFromResource(resource);
            const index = issues.value.findIndex(i => i['id'] === updatedIssue.id);
            if (index !== -1) issues.value[index] = updatedIssue;
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function deleteIssue(issue) {
        aldiApi.deleteIssue(issue.id).then(() => {
            const index = issues.value.findIndex(i => i['id'] === issue.id);
            if (index !== -1) issues.value.splice(index, 1);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function getServiceOrderById(id) {
        let idNum = parseInt(id);
        return serviceOrders.value.find(serviceOrders => serviceOrders['id'] === idNum);
    }
    
    function addServiceOrder(serviceOrder) {
        aldiApi.createServiceOrder(serviceOrder).then(response => {
            const resource = response.data;
            const newServiceOrder = ServiceOrdersAssembler.toEntityFromResource(resource);
            serviceOrders.value.push(newServiceOrder);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function updateServiceOrder(serviceOrder) {
        aldiApi.updateServiceOrder(serviceOrder).then(response => {
            const resource = response.data;
            const updatedServiceOrder = ServiceOrdersAssembler.toEntityFromResource(resource);
            const index = serviceOrders.value.findIndex(s => s['id'] === updatedServiceOrder.id);
            if (index !== -1) serviceOrders.value[index] = updatedServiceOrder;
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    function deleteServiceOrder(serviceOrder) {
        aldiApi.deleteServiceOrder(serviceOrder.id).then(() => {
            const index = serviceOrders.value.findIndex(s => s['id'] === serviceOrder.id);
            if (index !== -1) serviceOrders.value.splice(index, 1);
        }).catch(error => {
            errors.value.push(error);
        });
    }
    
    return {
        equipments,
        issues,
        serviceOrders,
        errors,
        equipmentsLoaded,
        issuesLoaded,
        serviceOrdersLoaded,
        equipmentsCount,
        issuesCount,
        serviceOrdersCount,
        fetchEquipments,
        fetchIssues,
        fetchServiceOrders,
        getEquipmentById,
        addEquipment,
        updateEquipment,
        deleteEquipment,
        getIssueById,
        addIssue,
        updateIssue,
        deleteIssue,
        getServiceOrderById,
        addServiceOrder,
        updateServiceOrder,
        deleteServiceOrder
    }
});

export default useAldiStore;