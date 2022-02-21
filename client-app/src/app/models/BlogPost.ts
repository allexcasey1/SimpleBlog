import { FormControl } from "@angular/forms";
import { v4 as uuid } from "uuid"

export interface BlogPost {
    id: string;
    title: string;
    imageUrl?: string;
    meta?: string;  // placeholder, not implemented
    content: Content;
}

export function CreateEmptyPost(): BlogPost {
    return {
        id: uuid(), 
        title: "",
        imageUrl: "",
        meta: "",
        content: {} as Content
    }
}


export interface Content {
    id: string;
    sections: [{
        id: string;
        sectionHeader: string;
        sectionText: string;
    }]
}



