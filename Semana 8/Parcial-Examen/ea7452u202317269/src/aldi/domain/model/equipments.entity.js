/**
 * Represents a Equipments entity.
 * @class
 * @author - Samuel Bonifacio - u202317269
 */
export class Equipments{
    
    constructor({id = null, name = "", costPerHour = 0, impactInStoreOperations = "", defaultAction = ""}){
        /**
         * Creates a new Equipments instance.
         * @param {Object} params - The parameters for the equipments.
         * @param {?number} [params.id=null] - The unique identifier for the equipments.
         * @param {string} [params.name=''] - The name of the equipments.
         * @param {number} [params.costPerHour=0] - The cost per hour of the equipments.
         * @param {string} [params.impactInStoreOperations=''] - The impact in store operations of the equipments.
         * @param {string} [params.defaultAction=''] - The default action of the equipments.
         * @type {null}
         */
        this.id = id;
        this.name = name;
        this.costPerHour = costPerHour;
        this.impactInStoreOperations = impactInStoreOperations;
        this.defaultAction = defaultAction;
    }
    
}