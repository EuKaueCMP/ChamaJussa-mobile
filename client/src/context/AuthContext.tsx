import { createContext, ReactNode, useEffect, useState } from "react";
import { TokenNetPayload, Usuario } from "../@types";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { jwtDecode } from 'jwt-decode'

interface AuthContextData {
    usuario: Usuario | null,
    estaCarregado: boolean,
    login: (token: string) => Promise<void>,
    logout: () => Promise<void>
}

export const AuthContext = createContext<AuthContextData | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [usuario, setUsuario] = useState<Usuario | null>(null);
    const [estaCarregado, setEstaCarregado] = useState<boolean>(true);

    useEffect(() => {
        async function carregarDadosSalvos() {
            try {
                const tokenSalvo = await AsyncStorage.getItem(process.env.EXPO_PUBLIC_TOKEN_KEY)
                if (tokenSalvo) {
                    const response = jwtDecode<TokenNetPayload>(tokenSalvo);
                    setUsuario({
                        id: response["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
                        nome: response["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
                        email: response["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
                    });
                }
            } catch (error) {
                console.error('Erro ao decodificar o token: ', error)
            } finally {
                setEstaCarregado(false);
            }
        }

        carregarDadosSalvos();
    }, []);

    const login = async (token: string) => {
        const response = jwtDecode<TokenNetPayload>(token);
        setUsuario({
            id: response["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
            nome: response["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
            email: response["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"]
        });
    };

    const logout = async () => {
        await AsyncStorage.removeItem(process.env.EXPO_PUBLIC_TOKEN_KEY);
        setUsuario(null);
    };

    return (
        <AuthContext.Provider value={{ usuario, estaCarregado, login, logout }}>
            {children}
        </AuthContext.Provider>
    )
}

