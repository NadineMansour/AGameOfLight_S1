class QuestionsController < ApplicationController
  before_filter :authenticate_teacher! 
  def create
    @question = Question.new question_params
    @question.teacher = current_teacher
    @subjects = Subject.all
    if @question.save
      render 'new', notice: 'Successfully created question.' 
    else
      render 'new', alert: 'Could not create question.'
    end
  end

  def new
    @question = Question.new
    @subjects = Subject.all
  end

  def index
    @questions = current_teacher.questions
  end

  def show
    @question = Question.find params[:id]
  end

  private

  def question_params
    params.require(:question).permit(:correct_answer, :subject_id, :wrong_answer_one, :wrong_answer_two, :wrong_answer_three)
  end
end
