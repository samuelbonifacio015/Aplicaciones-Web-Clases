import {ServiceOrders} from "@/aldi/domain/model/service-orders.entity.js";

/**
 * Assembler for converting API resources to Service Orders entities.
 * @class
 * @author Samuel Bonifacio - u202317269
 */
export class ServiceOrdersAssembler {

    /**
     * Converts a plain resource object to an Issues entity.
     * @param {Object} resource - The resource object representing a issues.
     * @returns {ServiceOrders} The corresponding Issues entity.
     */
    static toEntityFromResource(resource) {
        return new ServiceOrders({...resource})
    }

    /**
     * Converts an API response to an array of Service Orders entities.
     * Handles both array and object response formats.
     * Logs an error and returns an empty array if the response status is not 200.
     *
     * @returns {ServiceOrders[]} Array of ServiceOrders entities.
     * @param {import('axios').AxiosResponse} response - The API response containing service orders data.
     *
     */
    static toEntitiesFromResponse(response) {
        if (response.status !== 200) {
            console.error(`${response.status}, ${response.statusText}`);
            return [];
        }
        let resources = response.data instanceof Array ? response.data : response.data['service-orders'];

        return resources.map(resource => this.toEntityFromResource(resource));
    }
}