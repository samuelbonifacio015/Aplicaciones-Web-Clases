/**
 * Custom error class for validation errors in the CRM domain.
 * Extends the built-in Error class to provide a specific error type.
 */
export class ValidationError extends Error {
    constructor(message) {
        super(message);
        this.name = 'ValidationError';
    }
}