import {Equipments} from "@/aldi/domain/model/equipments.entity.js";

/**
 * Assembler for converting API resources to Equipment entities.
 * @class
 * @author Samuel Bonifacio - u202317269
 */
export class EquipmentsAssembler {

    /**
     * Converts a plain resource object to an Equipment entity.
     * @param {Object} resource - The resource object representing a equipment.
     * @returns {Equipment} The corresponding Equipment entity.
     */
    static toEntityFromResource(resource) {
        return new Equipments({...resource})
    }

    /**
     * Converts an API response to an array of Equipment entities.
     * Handles both array and object response formats.
     * Logs an error and returns an empty array if the response status is not 200.
     * 
     * @returns {Equipment[]} Array of Equipment entities.
     * @param {import('axios').AxiosResponse} response - The API response containing equipment data.
     *
     */
    static toEntitiesFromResponse(response) {
        if (response.status !== 200) {
            console.error(`${response.status}, ${response.statusText}`);
            return [];
        }
        let resources = response.data instanceof Array ? response.data : response.data['equipments'];

        return resources.map(resource => this.toEntityFromResource(resource));
    }
}