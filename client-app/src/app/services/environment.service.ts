import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IEnvironment } from 'src/environments/ienvironment';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService implements IEnvironment {
  get production() {
    return environment.production;
  }

  get enableDebugTools() {
    return environment.enableDebugTools;
  }

  get logLevel() {
    return environment.logLevel;
  }

  get apiHost() {
    return environment.apiHost;
  }

  get apiUrl() {
    return environment.apiUrl;
  }

  constructor() {}
}
