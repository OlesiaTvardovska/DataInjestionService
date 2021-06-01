import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular/apollo';
import { gql } from '@apollo/client/core';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
    // queryStr = "";
    // GETNEWS = gql`
    // query{
    //     unmarked_news(user_id: $userId){
    //       url
    //     }
    //   }
    // `;
    // constructor(private apollo: Apollo) { }

    // getUnmarkedData(userId: string): Observable<any> {
    //     return this.apollo.watchQuery<any>({
    //         query: this.GETNEWS,
    //         variables: {
    //             user_id: userId
    //         }
    //     }).valueChanges;
    // }
}