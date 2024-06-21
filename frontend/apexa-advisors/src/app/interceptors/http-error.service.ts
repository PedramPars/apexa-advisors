import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpHandlerFn,
  HttpInterceptor,
  HttpInterceptorFn,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { AppToastService } from '../toast.service';

export const httpInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  const toastService = inject(AppToastService);
  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {

      const errors = error.error.errors as []
      if (errors && errors.length)
        errors.forEach(e => toastService.show('Error', e['message']));
      return throwError(error);
    })
  );
};
