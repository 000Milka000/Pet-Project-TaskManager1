export interface AuthRequest{
    login: string;
    password: string;
}


export interface AuthResponse{
    id: number;
    name: string;
    login: string;
    token: string;
}