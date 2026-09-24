import React, { useEffect, useState } from 'react'
import { OrdemServico } from '../@types';
import { Alert } from 'react-native';
import { ordemServicoService } from '../services/ordemServicoService';

export const useOrdemServico = () => {
    const [os, setOs] = useState<OrdemServico[]>([])

    async function listarOs() {
        try {
            const dados = await ordemServicoService.listar();
            setOs(dados);
        } catch (error: any) {
            const mensagemErro = error?.response.data?.message
                || error?.response?.data
                || "Ocorreu um erro inesperado. Tente novamente"

            Alert.alert("Erro", mensagemErro)
        }
    }

    useEffect(() => {
        listarOs();
    }, [])

    return os;
}
