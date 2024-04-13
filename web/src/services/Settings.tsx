/** @format */

export class Settings {
	public static readonly GENERATOR_SERVICE_URL =
		process.env.GENERATOR_SERVICE_URL || 'http://localhost:12800';

	public static readonly NOTEBOOK_SERVICE_URL =
		process.env.NOTEBOOK_SERVICE_URL || 'http://localhost:12700';

	public static readonly FILE_SERVICE_URL =
		process.env.FILE_SERVICE_URL || 'http://localhost:12600';
}
