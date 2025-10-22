import {Issues} from "@/aldi/domain/model/issues.entity.js";

/**
 * Assembler for converting API resources to Issues entities.
 * @class
 * @author Samuel Bonifacio - u202317269
 */
export class IssuesAssembler {

    /**
     * Converts a plain resource object to an Issues entity.
     * @param {Object} resource - The resource object representing a issues.
     * @returns {Issues} The corresponding Issues entity.
     */
    static toEntityFromResource(resource) {
        return new Issues({...resource})
    }

    /**
     * Converts an API response to an array of Issues entities.
     * Handles both array and object response formats.
     * Logs an error and returns an empty array if the response status is not 200.
     *
     * @returns {Issues[]} Array of Issues entities.
     * @param {import('axios').AxiosResponse} response - The API response containing Issues data.
     *
     */
    static toEntitiesFromResponse(response) {
        if (response.status !== 200) {
            console.error(`${response.status}, ${response.statusText}`);
            return [];
        }
        let resources = response.data instanceof Array ? response.data : response.data['issues'];

        return resources.map(resource => this.toEntityFromResource(resource));
    }
}