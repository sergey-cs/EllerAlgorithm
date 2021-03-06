using System;

public class EullerAlgorithm
{
    public IEullerAlgorithmDataProvider Data = new DataProvider(10, 10);
    EullerAlgorithmSeeder Seeder = new EullerAlgorithmSeeder();

    public void Generate()
    {
        for (int i = 0; i < Data.GetWidth(); i++)
            Data.InsertDownBorder(i, Data.GetHeight() - 1);
        for (int i = 0; i < Data.GetHeight(); i++)
            Data.InsertRightBorder(Data.GetWidth() - 1, i);

        for (int i = 0; i < Data.GetHeight() - 1; i++)
        {
            FillGroups(i);
            CalculateRightBorders(i);
            CalculateBottomBorders(i);
            ProcessNextRow(i);
        }
        FillGroups(Data.GetHeight() - 1);
        CalculateRightBorders(Data.GetHeight() - 1);
        CalculateBottomBorders(Data.GetHeight() - 1);
    }

    void Borders()
    {
        for (int i = 0; i < Data.GetWidth(); i++)
            Data.InsertDownBorder(i, Data.GetHeight() - 1);
    }

    void FillGroups(int row)
    {
        for (int i = 0; i < Data.GetWidth(); i++)
            if (Data.GetGroup(i, row) == 0)
                Data.SetGroup(i, row, Data.GetNextGroup(i, row));
    }

    void CalculateRightBorders(int row)
    {
        for (int i = 0; i < Data.GetWidth() - 1; i++)
            if (Seeder.BetweenColumnBorder(i, i + 1, row))
                Data.InsertRightBorder(i, row);
            else Data.SetGroup(i + 1, row, Data.GetGroup(i, row));
    }
    void CalculateBottomBorders(int row)
    {
        for (int x = 0; x < Data.GetWidth(); x++)
            if (Data.CountGroupsInRowWithNoBorders(row, x) > 1 && Seeder.BetweenRowBorder(row, row + 1, x))
                Data.InsertDownBorder(x, row);
    }
    void ProcessNextRow(int row)
    {
        EullerAlgorithmDataProviderMethods.CopyGroupRow(Data, row, row + 1);
        for (int i = Data.GetWidth() - 1; i >= 0; i--)
            if (Data.ContainsBottomBorder(i, row))
                Data.SetGroup(i, row + 1, 0);
        FillGroups(row);
    }

}