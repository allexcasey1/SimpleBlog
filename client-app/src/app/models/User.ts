export interface UserResponse {
    email: string;
    username: string;
    displayName: string;
    token: string;
    expires: Date;
}

export class User {
    constructor(
        public email: string,
        public username: string, 
        public displayName: string, 
        private _token: string,
        private _expires: Date) {}

    get token(): string  {
        if (!this._expires || new Date() > this._expires) 
            return "";

        return this._token;
    }
}

export interface UserForm {
    email: string;
    password: string;
    displayName?: string | null;
    username?: string | null;
}

