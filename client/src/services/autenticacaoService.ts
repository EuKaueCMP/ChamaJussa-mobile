import AsyncStorage from "@react-native-async-storage/async-storage";
import { Login, LoginResponse } from "../@types";
import { api } from "./api";
import { useAuth } from "../hooks/useAuth";

export const autenticacaoSerive = {
    async login(dados: Login) {
        const { data } = await api.post<LoginResponse>("Autenticacao/login", dados);

        if (data.token) {
            //? token -> "localStorage" agr "AsyncStorage" no React-Native  
            await AsyncStorage.setItem(process.env.EXPO_PUBLIC_TOKEN_KEY, data.token);
        };
        return data;
    }
}