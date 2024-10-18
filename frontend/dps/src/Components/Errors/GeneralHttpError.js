class GeneralHttpError extends Error {
    constructor(details) {
        super("Something went wrong while performing an HTTP invocation.");
        this.name = "GeneralHttpError";
        this.details = details; 
    }
}