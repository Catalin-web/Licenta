/** @format */

import axios from 'axios';
import { Settings } from '../Settings';
import {
	LoginRequest,
	RegisterRequest,
	User,
} from './UserServiceModels';

export class UserService {
	private readonly baseUrl: string;
	constructor() {
		this.baseUrl = Settings.USER_SERVICE_URL;
	}

	public async registerAsync(
		registerRequest: RegisterRequest,
	): Promise<User> {
		let response = await axios.post<User>(
			`${this.baseUrl}/userService/register`,
			registerRequest,
		);
		if (response.status !== 200) {
			throw new Error(response.statusText);
		}
		let user = response.data;
		localStorage.setItem('userId', user.id);
		return user;
	}
	public async loginAsync(loginRequest: LoginRequest): Promise<User> {
		let response = await axios.post<User>(
			`${this.baseUrl}/userService/login`,
			loginRequest,
		);
		if (response.status !== 200) {
			throw new Error(response.statusText);
		}
		let user = response.data;
		localStorage.setItem('userId', user.id);
		return user;
	}

	public async getUserById(userId: string): Promise<User> {
		let response = await axios.get<User>(
			`${this.baseUrl}/userService/user/${userId}`,
		);
		if (response.status !== 200) {
			throw new Error(response.statusText);
		}
		return response.data;
	}

	public async getLoggedUser(): Promise<User | null> {
		let userId = localStorage.getItem('userId');
		if (userId) {
			return this.getUserById(userId);
		}
		return null;
	}
}
