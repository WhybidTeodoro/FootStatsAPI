const API_BASE_URL = "https://localhost:7198/api";


async function getTeams() {
    try {
        const response = await fetch(`${API_BASE_URL}/team`);

        if (!response.ok) {
            throw new Error("Erro ao buscar times na API");
        }

        const data = await response.json();

        console.log("Resposta da API:", data);

        // 👇 IMPORTANTE
        renderTeams(data.items);

    } catch (error) {
        console.error("Erro na requisição:", error);
    }
}

function renderTeams(teams){

    const table = document.getElementById("teamsTable");

    if(!table) return;

    table.innerHTML = "";

    if (!Array.isArray(teams)) {
        console.error("teams não é um array:", teams);
        return;
    }

    let rows = "";

    teams.forEach(team => {
        rows += `
            <tr>
                <td>${team.id}</td>
                <td>${team.name}</td>
            </tr>`;
    });

    table.innerHTML = rows;
}

document.addEventListener("DOMContentLoaded", () =>{
    getTeams();
})