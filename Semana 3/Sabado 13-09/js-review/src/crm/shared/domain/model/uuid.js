import { v4 as uuidv4, validate as uuidValidate } from 'uuid';

export function generateUUID() {
    return uuidv4();
}

export function validateUUID(value) {
    return uuidValidate(value);
}