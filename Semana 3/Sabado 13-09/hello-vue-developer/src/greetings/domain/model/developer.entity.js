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
     * @param {string} firstName - The developer's first name.
     * @param {string} lastName - The developer's last name.
     */
    constructor(firstName, lastName) {
        this.#firstName = firstName?.trim() || "";
        this.#lastName = lastName?.trim() || "";
    }

    /**
     * Gets the developer's first name.
     * @returns {string}
     */
    get firstName() {
        return this.#firstName;
    }

    /**
     * Gets the developer's last name.
     * @returns {string}
     */
    get lastName() {
        return this.#lastName;
    }

    /**
     * Gets the developer's full name.
     * @returns {string|string}
     */
    get fullName() {
        return this.#firstName && this.#lastName ? `${this.#firstName} ${this.#lastName}` : "Unknown Developer";
    }
}