import axios,{
    AxiosResponse,
    AxiosError
} from "../../node_modules/axios/index"


interface Conversation {
    question:string,
    id:number,
    timeOfConversation:string
}

axios.get<Conversation[]>("https://conversationrestservice20191209113100.azurewebsites.net/api/conversations").then((response:AxiosResponse)=>{

document.getElementById("mainBody").innerHTML+="<ul>";
response.data.forEach((element:Conversation) => {
    document.getElementById("mainBody").innerHTML+="<li>"+element.question+"</li>"
});

document.getElementById("mainBody").innerHTML+="</ul>";

})