const featuredUploadElement = document.getElementById("featuredImageUpload"); 
const featuredImageUrlElement = document.getElementById("featuredImageUrl"); 
const featuredImageDisplayElement = document.getElementById("featuredImageDisplay"); 

featuredUploadElement.addEventListener("change", uploadFeaturedImage); 

async function uploadFeaturedImage(e) {
    const file = e.target.files[0];
    if (!file) return; 

    const data = new FormData(); 
    data.append("file", file); 

    const response = await fetch("/api/images", {
        method: "POST", 
        headers: { "Accept": "*/*" },
        body: data
    });

    if (!response.ok) {
        alert("Upload non riuscito. Controlla estensione e riprova.");
        return; 
    }

    const result = await response.json(); 

    featuredImageUrlElement.value = result.link; 

    featuredImageDisplayElement.scr = result.link; 
    featuredImageDisplayElement.style.display = "block"; 
}

