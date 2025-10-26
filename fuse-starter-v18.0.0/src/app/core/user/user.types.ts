export interface User
{
    id: string;
    name: string;
    email: string;
    avatar?: string;
    status?: string;
    companyId?: number; // 🏢 Add CompanyId to user interface
}
