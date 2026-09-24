import { useEffect, useState } from "react";
import { Alert, StyleSheet, Text, TextInput, TouchableOpacity, View } from "react-native"
import { Image } from "expo-image"
import { useRouter } from "expo-router";
import { autenticacaoSerive } from "../../services/autenticacaoService";
import { useAuth } from "../../hooks/useAuth";
import AsyncStorage from "@react-native-async-storage/async-storage";

export default function Login() {
    const { login } = useAuth();
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    const router = useRouter();

    async function acessar() {
        const emailDigitado = email.trim();
        const senhaDigitada = senha.trim();

        if (!emailDigitado || !senhaDigitada) {
            Alert.alert("Por favor, preencha todos os campos!")
            return;
        }

        try {
            const response = await autenticacaoSerive.login({ email: emailDigitado, senha: senhaDigitada })

            if (response && response.token)
                await login(response.token)

            router.replace("/(tabs)/listaOs");
        } catch (error: any) {
            const mensagemErro =
                error.response?.data?.header?.message ||
                error.response?.data?.message ||
                error.response?.data ||
                error.message ||
                "deu erro aqui na login"

            console.log(mensagemErro);
            Alert.alert("Erro, E-mail ou senha inválidos!")
        }
    }

    return (
        <View style={styles.container}>
            <Image source={require("../../../assets/imgs/logo.svg")} style={styles.logo} />
            <View style={styles.container_form}>
                <View style={styles.container_title}>
                    <Text style={{ fontFamily: 'Montserrat_700Bold', fontSize: 20, marginBottom: '2%' }}>Chama Jussa</Text>
                    <Text style={{ fontFamily: 'Montserrat_400Regular', fontSize: 12 }}>Gerenciamento de Ordens de Servico</Text>
                </View>
                <View style={styles.container_inputs}>
                    <View>
                        <Text style={styles.input_label}>E-mail</Text>
                        <TextInput style={styles.text_input}
                            placeholder="email@email.com"
                            value={email}
                            textContentType="emailAddress"
                            onChangeText={setEmail} />
                    </View>
                    <View>
                        <Text style={styles.input_label}>Senha</Text>
                        <TextInput style={styles.text_input}
                            placeholder="Digite sua senha"
                            secureTextEntry
                            value={senha}
                            textContentType="password"
                            onChangeText={setSenha} />
                    </View>
                </View>
                <TouchableOpacity style={styles.btn_login} onPress={acessar}>
                    <Text style={{ 'fontWeight': '600', color: '#fff', fontFamily: 'Montserrat_600SemiBold' }}>Acessar o sistema</Text>
                </TouchableOpacity>
            </View>
        </View >
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#F3F4F6',
        alignItems: 'center',
        justifyContent: 'flex-start'
    },

    logo: {
        width: 250,
        height: 250,
        marginTop: '15%',
    },

    container_form: {
        backgroundColor: '#FFFFFF',
        borderRadius: 10,
        alignItems: 'center',
        boxShadow: '0px 6px 4px rgba(0, 0, 0, 0.28)',
        width: '80%',
        height: '50%'
    },

    container_title: {
        alignItems: 'center',
        marginTop: '10%',
        marginBottom: '10%'
    },

    input_label: {
        fontFamily: 'Montserrat_600SemiBold'
    },

    container_inputs: {
        alignSelf: 'center',
        width: '80%',
        height: '40%',
        justifyContent: 'space-between'
    },

    text_input: {
        borderColor: '#e4e3e3',
        borderRadius: 5,
        backgroundColor: '#fff',
        color: '#03000096',
        fontFamily: 'Montserrat_400Regular',

        alignSelf: 'center',
        width: '100%',
        borderWidth: 1,
        padding: 14
    },

    btn_login: {
        alignSelf: 'center',
        width: '80%',
        padding: 16,
        borderRadius: 5,
        backgroundColor: '#10B981',
        alignItems: 'center',
        marginTop: '6%'
    },
});
