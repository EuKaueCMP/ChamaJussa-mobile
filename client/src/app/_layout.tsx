import { Montserrat_400Regular, Montserrat_600SemiBold, Montserrat_700Bold, useFonts } from '@expo-google-fonts/montserrat';
import { Stack } from 'expo-router'
import { StatusBar } from 'expo-status-bar';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import { AuthProvider } from '../context/AuthContext';

const RootLayout = () => {
    const [loaded] = useFonts({
        Montserrat_400Regular,
        Montserrat_600SemiBold,
        Montserrat_700Bold,
    });

    if (!loaded) {
        return null;
    }

    return (
        <AuthProvider>
            <SafeAreaProvider>
                <StatusBar style="dark" />
                <Stack>
                    <Stack.Screen
                        name="login/index"
                        options={{
                            title: "login",
                            headerShown: false,
                        }}
                    />
                    <Stack.Screen
                        name="detalhesOs/[id]"
                        options={{
                            title: "Detalhes da OS",
                            headerShown: false,
                        }}
                    />
                    <Stack.Screen
                        name="(tabs)"
                        options={{
                            headerShown: false,
                        }}
                    />
                </Stack>
            </SafeAreaProvider>
        </AuthProvider>
    )
}

export default RootLayout;