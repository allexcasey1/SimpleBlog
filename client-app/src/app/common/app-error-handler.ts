import { ErrorHandler } from "@angular/core";

export class AppErrorHandler extends ErrorHandler {
    override handleError(error: any) {
        // alert("An unexpected error has occured.");
        console.log(error);
    }
}