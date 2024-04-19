/** @format */

export interface RegisterRequest {
	firstName: string;
	lastName: string;
	email: string;
	password: string;
}

export interface LoginRequest {
	email: string;
	password: string;
}

export interface User {
	id: string;
	email: string;
	password: string;
	firstName: string;
	lastName: string;
}
