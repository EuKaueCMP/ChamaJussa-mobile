import { StatusBar } from 'expo-status-bar';
import { StyleSheet } from 'react-native';
import { SafeAreaProvider, SafeAreaView } from 'react-native-safe-area-context';
import { Montserrat_400Regular, Montserrat_600SemiBold, Montserrat_700Bold, useFonts } from '@expo-google-fonts/montserrat'
import Login from './src/app/login';
import { AuthProvider } from './src/context/AuthContext';

export default function App() {
  const [loaded] = useFonts({
    Montserrat_400Regular,
    Montserrat_600SemiBold,
    Montserrat_700Bold
  })

  if (!loaded)
    return null;

  return (
    // SafeAreaProvider -> calcular o tamanho das extremidades (topo e rodape)
    <AuthProvider>
      <SafeAreaProvider>
        {/* // SafeAreaView -> aplica o padrao de margem necessaria para o tamanho do celular */}
        <SafeAreaView style={styles.safeAreaView}>
          {/* <StatusBar style='dark'/>
        <StatusBar style='light'/> */}
          <StatusBar style='auto' />
          <Login />
        </SafeAreaView>
      </SafeAreaProvider>
    </AuthProvider>
  );
}

const styles = StyleSheet.create({
  safeAreaView: {
    flex: 1,
  },
});