using Bibliotheca.Domain;

namespace Bibliotheca.Data;

public interface ILiteraryWorkRepository
{
    List<LiteraryWork> ListAllWorks();
    LiteraryWork? FindWorkById(int workId);
    int CreateWork(LiteraryWork work);
    int UpdateWork(LiteraryWork work);
    int DeleteWork(int workId);
}
