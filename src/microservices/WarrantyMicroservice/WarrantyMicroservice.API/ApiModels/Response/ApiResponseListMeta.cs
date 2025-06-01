namespace WarrantyMicroservice.API.ApiModels.Response;

public record ApiResponseListMeta(
    int CurrentPage,
    int PerPage,
    int Total);