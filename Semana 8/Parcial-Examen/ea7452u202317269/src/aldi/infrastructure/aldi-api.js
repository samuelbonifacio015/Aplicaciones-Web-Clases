import {BaseApi} from "@/shared/infrastructure/base-api.js";
import {BaseEndpoint} from "@/shared/infrastructure/base-endpoint.js";

const equipmentsEndpointPath    = import.meta.env.VITE_EQUIPMENTS_ENDPOINT_PATH;
const issuesEndpointPath        = import.meta.env.VITE_ISSUES_ENDPOINT_PATH;
const serviceOrdersEndpointPath = import.meta.env.VITE_SERVICESORDERS_ENDPOINT_PATH;
export class AldiApi extends BaseApi {
    /**
     * @type {BaseEndpoint}
     * @private
     */
    #equipmentsEndpoint;
    /**
     * @type {BaseEndpoint}
     * @private
     */
    #issuesEndpoint;
    /**
     * @type {BaseEndpoint}
     * @private
     */
    #serviceOrdersEndpoint;
    
    /**
     * Initializes endpoints for categories and tutorials.
     */

    constructor() {
        super();
        this.#equipmentsEndpoint = new BaseEndpoint(this,  equipmentsEndpointPath);
        this.#issuesEndpoint = new BaseEndpoint(this, issuesEndpointPath);
        this.#serviceOrdersEndpoint = new BaseEndpoint(this, serviceOrdersEndpointPath);
    }

    /**
     * Fetches all equipments.
     * @returns {Promise<import('axios').AxiosResponse>} Promise resolving to the equipments' response.
     */
    getEquipments() {
        return this.#equipmentsEndpoint.getAll();
    }
    
    getEquipmentsById(id) {
        return this.#equipmentsEndpoint.getById(id);
    }
    
    createEquipment(resource) {
        return this.#equipmentsEndpoint.create(resource);
    }
    
    updateEquipment(id, resource) {
        return this.#equipmentsEndpoint.update(id, resource);
    }
    
    deleteEquipment(id) {
        return this.#equipmentsEndpoint.delete(id);
    }

    getIssues() {
        return this.#issuesEndpoint.getAll();
    }
    
    getIssuesById(id) {
        return this.#issuesEndpoint.getById(id);
    }
    
    createIssue(resource) {
        return this.#issuesEndpoint.create(resource);
    }
    
    updateIssue(id, resource) {
        return this.#issuesEndpoint.update(id, resource);
    }
    
    deleteIssue(id) {
        return this.#issuesEndpoint.delete(id);
    }

    getServiceOrders() {
        return this.#serviceOrdersEndpoint.getAll();
    }
    
    getServiceOrdersById(id) {
        return this.#serviceOrdersEndpoint.getById(id);
    }
    
    createServiceOrder(resource) {
        return this.#serviceOrdersEndpoint.create(resource);
    }
    
    updateServiceOrder(id, resource) {
        return this.#serviceOrdersEndpoint.update(id, resource);
    }
}