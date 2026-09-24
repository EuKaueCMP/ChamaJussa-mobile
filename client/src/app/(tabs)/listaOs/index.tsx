import { Alert, FlatList, Pressable, Text, TouchableOpacity, View } from "react-native"
import { styles } from "./listaOs.styles"
import { useState } from "react";
import { useOrdemServico } from "../../../hooks/useOrdemServico";
import CardOs from "../../../components/cardOs";
import { SafeAreaView } from "react-native-safe-area-context";

// export const ListaOs = () => {
export default function ListaOs() {
    const os = useOrdemServico();

    return (
        <>
            <View style={styles.container}>
                <View style={styles.superior}>
                    <View>
                        <Text style={styles.titulo}>Olá, Késsia</Text>
                        <Text style={styles.titulo_lista}>Minhas OSs</Text>
                    </View>
                    {/* Use o TouchableOpacity para protótipos rápidos e botões simples onde o efeito padrão de transparência já atende bem.
        Use o Pressable para criar sistemas de design robustos, botões com feedback de cor customizado e componentes que precisam de respostas ao toque mais complexas. */}
                    <TouchableOpacity style={styles.btn_nova_os}>
                        <Text style={styles.btn_text}>Nova OS</Text>
                    </TouchableOpacity>
                </View>
                <View style={styles.filtros}>
                    <Pressable style={styles.filterbtn}>
                        <Text style={styles.filterbtntxt}>Todos</Text>
                    </Pressable>
                    <Pressable style={styles.filterbtn}>
                        <Text style={styles.filterbtntxt}>Aberto</Text>
                    </Pressable>
                    <Pressable style={styles.filterbtn}>
                        <Text style={styles.filterbtntxt}>Em Andamento</Text>
                    </Pressable>
                    <Pressable style={styles.filterbtn}>
                        <Text style={styles.filterbtntxt}>Concluídas</Text>
                    </Pressable>
                </View>
                <FlatList
                    data={os}
                    keyExtractor={(item) => String(item.osId)}
                    showsVerticalScrollIndicator={false}
                    renderItem={({ item }) => (
                        //card:
                        <CardOs
                            numOs={item.osId}
                            status={item.statusNome}
                            titulo={item.nomeItem}
                            descricao={item.descricao}
                        />
                    )}
                />
            </View >
        </>
    )
}