const API_BASE_URL = "https://localhost:7198/api";

let currentPage = 1;
let pagesize = 10;

let currentName = "";
let currentSortBy = "name";

async function getTeams() {
    try {

        let url = `${API_BASE_URL}/team?pageNumber=1&pagesize=20 `;

        if(currentName){
            url += `&name=${encodeURIComponent(currentName)}`;
        }

        if(currentSortBy){
            url += `&sortBy=${currentSortBy}`;
        }

        console.log("Url da requisição", url);

        const response = await fetch(url);

        if (!response.ok) {
            throw new Error("Erro ao buscar times na API");
        }

        const data = await response.json();

        console.log("Resposta da API:", data);

        renderTeams(data.items);
        renderPagination(data);

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

    currentName = document.getElementById("teamName").value;
    currentSortBy = document.getElementById("sortBy").value;

    currentPage = 1;

    getTeams();
}

function renderPagination(data){
    const pageInfo = document.getElementById("pageInfo");
    const nextBtn = document.getElementById("nextBtn");

    if(!pageInfo || !nextBtn) return;

    const totalPages = Math.ceil(data.totalCount / data.pageSize);

    pageInfo.innerText = `Página ${data.pageNumber} de ${totalPages}`;

    nextBtn.disabled = data.pageNumber >= totalPages;
}

document.addEventListener("DOMContentLoaded", () =>{
    getTeams();

    const form = document.getElementById("teamForm");

    if(form){
        form.addEventListener("submit", handleSearch);
    }

    const prevBtn = document.getElementById("prevPage");
    const nextBtn = document.getElementById("nextPage");


    if(prevBtn){
        prevBtn.addEventListener("click", () =>{
            if(currentPage > 1){
                currentPage--;
                getTeams();
            }
        });
    }

    if(nextBtn){
        nextBtn.addEventListener("click", () =>{
            currentPage++;
            getTeams();
        });
    }
});