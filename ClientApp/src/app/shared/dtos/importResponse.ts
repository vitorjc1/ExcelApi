import { ImportError } from './importError';

export interface ImportResponse {
    importId: number,
    success: boolean,
    errors: ImportError[]
}