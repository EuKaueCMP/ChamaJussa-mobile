import { SafeAreaView } from "react-native-safe-area-context";
import { Text, View, Image, TouchableOpacity } from "react-native";
import { styles } from "./perfil.style"
import { useRouter } from "expo-router";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { useAuth } from "../../../hooks/useAuth";
import ImgUsuario from "../../../components/imgUsuario";

export default function Perfil() {
  const router = useRouter();
  const { usuario, logout } = useAuth();

  console.log("dados do usuario logado: ", usuario)

  async function signout() {
    logout();
    router.replace("/login")
  }

  return (
    <SafeAreaView style={styles.safeArea}>
      <View style={styles.container}>
        {/* Título Principal */}
        <Text style={styles.headerTitle}>Perfil</Text>

        {/* Card de Informações do Usuário */}
        <View style={styles.card}>
          <ImgUsuario />
          <Text style={styles.userName}>{usuario?.nome}</Text>
          <Text style={styles.userEmail}>{usuario?.email}</Text>
        </View>

        {/* Botão de Sair da Conta */}
        <TouchableOpacity onPress={() => signout()}
          style={styles.logoutButton}
          activeOpacity={0.8}
        >
          <Text style={styles.logoutButtonText}>Sair da Conta</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  )
}