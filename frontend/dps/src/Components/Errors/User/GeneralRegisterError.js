class GeneralRegisterError extends Error {
    constructor(details) {
        super("Something went wrong while attempting to register.");
        this.name = "GeneralRegisterError";
        this.details = details; 
    }
}