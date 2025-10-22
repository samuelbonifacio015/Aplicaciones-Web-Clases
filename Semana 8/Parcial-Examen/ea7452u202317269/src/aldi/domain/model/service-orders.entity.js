/**
 * Represents a service order in the system.
 * @class
 * @author - Samuel Bonifacio - u202317269
 */
export class ServiceOrders {
    constructor(id = null, equipmentId = null, issueId = null, neededAction = "", 
                priority = "", registeredAt = null,) {
        /**
         * Creates a new ServiceOrders instance.
         * @param {Object} params - The parameters for the service order.
         * @param {?number} [params.id=null] - The unique identifier for the service order.
         * @param {?number} [params.equipmentId=null] - The equipment ID associated with the service order.
         * @param {?number} [params.issueId=null] - The issue ID associated with the service order.
         * @param {string} [params.neededAction=''] - The action needed for the service order.
         * @param {string} [params.priority=''] - The priority level of the service order.
         * @param {?Date} [params.registeredAt=null] - The date when the service order was registered.
         * @type {null}
         */
        this.id = id;
        this.equipmentId = equipmentId;
        this.issueId = issueId;
        this.neededAction = neededAction;
        this.priority = priority;
        this.registeredAt = registeredAt;
    }
}