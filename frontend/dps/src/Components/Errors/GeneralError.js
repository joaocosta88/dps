class GeneralError extends Error {
    constructor(details) {
        super("Something went wrong.");
        this.name = "GeneralError";
        this.details = details; 
    }
}