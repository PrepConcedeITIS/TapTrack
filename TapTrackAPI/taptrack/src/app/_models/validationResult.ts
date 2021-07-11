export class ValidationResult<T> {
    value: T;
    isSuccess: boolean;
    validationKey: string;
    message: string;

    constructor(value: T, isSuccess: boolean, validationKey: string, message: string) {
        this.value = value;
        this.isSuccess = isSuccess;
        this.validationKey = validationKey;
        this.message = message;
    }
}
