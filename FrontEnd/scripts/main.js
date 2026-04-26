const API_BASE_URL = "https://localhost:7198/api";


async function getTeams(name = "", sortBy = "name") {
    try {

        let url = `${API_BASE_URL}/team?pageNumber=1&pagesize=20 `;

        if(name){
            url += `&name=${encodeURIComponent(name)}`;
        }

        if(sortBy){
            url += `&sortBy=${sortBy}`;
        }

        console.log("Url da requisição", url);

        const response = await fetch(url);

        if (!response.ok) {
            throw new Error("Erro ao buscar times na API");
        }

        const data = await response.json();

        console.log("Resposta da API:", data);

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

function handleSearch(event){
    event.preventDefault();

    const name = document.getElementById("teamName").value;
    const sortBy = document.getElementById("sortBy").value;

    getTeams(name, sortBy);
}

document.addEventListener("DOMContentLoaded", () =>{
    getTeams();

    const form = document.getElementById("teamForm");

    if(form){
        form.addEventListener("submit", handleSearch);
    }
});