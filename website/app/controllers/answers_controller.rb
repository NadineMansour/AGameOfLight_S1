class AnswersController < InheritedResources::Base

  private

    def answer_params
      params.require(:answer).permit(:belongs_to, :belongs_to, :ans, :correct)
    end
end

