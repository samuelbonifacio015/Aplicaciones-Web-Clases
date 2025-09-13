/**
 * Represents a developer with a first name and last name.
 */
export class Developer {
    /**
     * @type {string}
     * @private
     */
    #firstName;
    /**
     * @type {string}
     * @private
     */
    #lastName;

    /**
     * Creates a new Developer instance.
     * @param {string} firstName - The first name of the developer.
     * @param {string} lastName - The last name of the developer.
     */
    constructor(firstName, lastName) {
        this.#firstName = firstName?.trim() ||"";
        this.#lastName = lastName.trim() ||"";
    }

    /**
     * Gets the first name of the developer.
     * @returns {string} The first name.
     */
    get firstName() {
        return this.#firstName;
    }

    /**
     * Gets the last name of the developer.
     * @returns {string} The last name.
     */
    get lastName() {
        return this.#lastName;
    }

    get fullName() {
        return this.#firstName && this.#lastName ? `${this.#firstName} ${this.#lastName}` : "Unkwnown Developer";
    }
}