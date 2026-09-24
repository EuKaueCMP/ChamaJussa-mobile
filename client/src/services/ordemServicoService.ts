import { OrdemServico } from "../@types";
import { api } from "./api";

export const ordemServicoService = {
    async listar(): Promise<OrdemServico[]> {
        const responsta = await api.get<OrdemServico[]>("OrdemServico")
        return responsta.data;
    },

    async buscarPorId(id: string | number): Promise<OrdemServico> {
        const resposta = await api.get<OrdemServico>("OrdemServico/" + id);
        return resposta.data;
    }
}