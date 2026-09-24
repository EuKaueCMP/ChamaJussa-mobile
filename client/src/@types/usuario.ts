export interface Usuario {
    id: string | number,
    nome: string,
    email: string
}

// 2. Crie uma interface interna para tipar o que REALMENTE vem do seu .NET JWT
export interface TokenNetPayload {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string;
  exp: number;
  iss: string;
  aud: string;
}