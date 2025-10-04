export interface BaseResponseModel<T> {
    Success: boolean;
    Message?: string;
    ErrorCode?: string;
    Data?: T;
    Errors?: string[];
  }