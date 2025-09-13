export class DateTime {
    #date;

    constructor(date = new Date()) {
        const parsedDate = date instanceof Date ? date : new Date(date);
        if (isNaN(parsedDate .getTime())) {
            throw new Error('Invalid date format');
        }
        this.#date = parsedDate;
    }

    get date() {
        return new Date(this.#date);
    }

    toISOString() {
        return this.#date.toISOString();
    }

    toString() {
        let options = { year: 'numeric', month: 'numeric',
            day: 'numeric', hour: 'numeric', minute: 'numeric', hour12: true };
        return this.#date.toLocaleString('en-US', options);
    }

    equals(other) {
        return other instanceof DateTime && this.#date.getTime() === other.#date.getTime();
    }
}