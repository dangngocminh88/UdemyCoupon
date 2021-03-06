import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class DataService {

    private REST_API_SERVER = "https://localhost:44351/";

    constructor(private httpClient: HttpClient) { }

    handleError(error: HttpErrorResponse) {
        let errorMessage = 'Unknown error!';
        if (error.error instanceof ErrorEvent) {
          // Client-side errors
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // Server-side errors
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        window.alert(errorMessage);
        return throwError(errorMessage);
      }

    public sendGetRequest(api: string) {
      return this.httpClient.get(`${this.REST_API_SERVER}${api}`).pipe(catchError(this.handleError));
    }
}