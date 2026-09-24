import { Text, View } from "react-native";
import { useAuth } from "../../hooks/useAuth";
import { styles } from "./imgUsuario.style";

export default function ImgUsuario() {
    const { usuario } = useAuth();
    const arrNomeUsuario: string[] = usuario?.nome.split(" ") ?? [];
    const primeiroNome = arrNomeUsuario[0] ? arrNomeUsuario[0].charAt(0).toUpperCase() : "A";
    const segundoNome = arrNomeUsuario[1] ? arrNomeUsuario[1].charAt(0) : "";

    return (
        <View style={styles.avatar}>
            <Text style={styles.text}>
                {`${primeiroNome}${segundoNome}`}
            </Text>
        </View>
    )
}

