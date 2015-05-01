class QuestionsController < ApplicationController
  before_filter :authenticate_teacher! , :redirect_if_not_verified_teacher
  def create
    @question = Question.new question_params
    @question.teacher = current_teacher
    @subjects = current_teacher.subjects
    if @question.save
      redirect_to action: "new" , notice: 'Successfully created question.' 
    else
      render 'new', alert: 'Could not create question.'
    end
  end

  def new
    @question = Question.new
    @subjects =current_teacher.subjects
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
  def redirect_if_not_verified_teacher
    unless current_teacher.verified
      redirect_to :back,notice:"sorry you are not verified"
    end
  end
end
