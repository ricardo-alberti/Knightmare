interface ISearch
{
    Node BestTree(Board _position,
                  int depth,
                  int alpha,
                  int beta,
                  bool isMaximizing);
}
