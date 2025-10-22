/**
 * Represents an issue related to equipment.
 * @class
 * @author - Samuel Bonifacio - u202317269
 */
export class Issues {
    constructor(id = null, equipmentId = null, issueType = "", registeredAt = null, status = "") {
        /**
         * Creates a new IssuesEntity instance.
         * @param {Object} params - The parameters for the issue.
         * @param {?number} [params.id=null] - The unique identifier for the issue.
         * @param {?number} [params.equipmentId=null] - The equipment ID associated with the issue.
         * @param {string} [params.issueType=''] - The type of the issue.
         * @param {?Date} [params.registeredAt=null] - The date when the issue was registered.
         * @param {string} [params.status=''] - The status of the issue.
         * @type {null}
         */
        this.id = id;
        this.equipmentId = equipmentId;
        this.issueType = issueType;
        this.registeredAt = registeredAt;
        this.status = status;
    }
}