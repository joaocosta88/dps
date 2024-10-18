class EmailAlreadyRegisteredError extends Error {
    constructor(details) {
        super("Email already registered.");
        this.name = "EmailAlreadyRegisteredError";
        this.details = details;
    }
}